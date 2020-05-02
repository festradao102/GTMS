using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GTMS.Data;
using GTMS.Models;
using Apache.NMS;
using Apache.NMS.Util;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Util;
using Apache.NMS.ActiveMQ.Transport.Tcp;
using Apache.NMS.ActiveMQ.Transport;
using Apache.NMS.ActiveMQ.Commands;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GTMS.Controllers
{
    public class PlayerController : Controller
    {

        private readonly GtmsDbContext _context;

        public PlayerController(GtmsDbContext context)
        {
            _context = context;
        }

        // GET: Player
        public async Task<IActionResult> Index()
        {
            return View(await _context.Players.ToListAsync());
        }

        // GET: Player/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .FirstOrDefaultAsync(m => m.PlayerID == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Player/Create
        public IActionResult Create()
        {
            ViewBag.ListOfTeams = getTeamsSelectList();
            ViewBag.ListOfConfigValues = getPositionsSelectList();
            return View();
        }

        // POST: Player/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlayerID,Identification,TeamName,Name,LastName,Age,Height,Weight,Email,Position")] Player player)
        {           
            if (ModelState.IsValid)
            {
                SendQueueMessages(player);

                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

        // GET: Player/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            ViewBag.ListOfTeams = getTeamsSelectList();
            ViewBag.ListOfConfigValues = getPositionsSelectList();
            return View(player);
        }

        // POST: Player/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlayerID,Identification,TeamName,Name,LastName,Age,Height,Weight,Email,Position")] Player player)
        {
            if (id != player.PlayerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(player);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.PlayerID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

        // GET: Player/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .FirstOrDefaultAsync(m => m.PlayerID == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _context.Players.FindAsync(id);
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.PlayerID == id);
        }

        private List<SelectListItem> getTeamsSelectList()
        {
            List<Team> teamsList = _context.Teams.ToList();

            // clase <SelectListItem> para llenar un elemento en la(s) vista(s). Tiene las propiedades text, value, selected.
            List<SelectListItem> list = teamsList.ConvertAll( a =>
            {
                return new SelectListItem()
                {
                    Text = a.TeamName,
                    Value = a.TeamName,
                    Selected = false
                };
            });
            return list;
        }

        private List<SelectListItem> getPositionsSelectList()
        {
            List<ConfigValues> valueList = _context.ConfigValues.ToList();

            List<SelectListItem> list = valueList.ConvertAll( a =>
            {
                return new SelectListItem()
                {
                    Text = a.Description,
                    Value = a.Description,
                    Selected = false
                };
            });
            return list;
        }    

        // GET: Player/SendEmail
        public async Task<IActionResult> SendEmail()
        {
            return View(await _context.Players.ToListAsync());
        }

        // accion de Player/SendEmail
        public async Task<ActionResult> SendEmailToPlayer()
        { 
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                                        
                using (var client = new HttpClient(clientHandler)){  

                    //Hosted web API REST Service base url -- MessageApi
                    string Baseurl = "https://localhost:5003"; 

                    //pasar el url del servicio restapi
                    client.BaseAddress = new Uri(Baseurl);  
        
                    //clear el http header para cada request
                    client.DefaultRequestHeaders.Clear();  

                    //definir tipo formato del request, pareciera no importar para strings de xml 
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));  

                    //enviar request para encontrar el recurso "api/ReceivedMessage" usando HttpClient del .net framework
                    HttpResponseMessage Res = await client.GetAsync("api/ReceivedMessage");  
        
                    //verificar si la respuesta es exitosa o no  
                    if (Res.IsSuccessStatusCode)  
                    {                    
                        var response = Res.Content.ReadAsStringAsync().Result;  //guardar los detalles de la respuesta del api      
                    }                 
                    return View();  
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Internal server Error");                
                throw e;
            }           
        }        

        //enviar mensajes al servicio de comunicacion/mensajeria
         public void SendQueueMessages(Player message){

            // definir nombre del queue, en caso de no existir activemq lo crea
            // definir el uri del endpoint de aws mq - aws acepta ssl no tcp asi que el string cambia en el protocolo
            // al ser ssl hay que enviar las credenciales
            // hay que configurar en aws console para que el VPC acepte trafico de afuera, agregar una politica en el security group para inbound traffic
            // connectionfactory del package apache.nms provee manejo de comunicacion con el queue
            // definir el "producer" en caso de la clase que envia de mensaje 
            // definir el "receiver" en caso de ser el servicio(s) consumiendo mensajes del queue 

            string queueName = "dev_queue"; 
            Console.WriteLine($"Adding message to queue topic: {queueName}");      
            string brokerUri = $"activemq:tcp://localhost:61616";  // dev broker

            //string brokerUri = $"activemq:ssl://b-57e8bf3e-69c9-4bec-b528-de407901bd09-1.mq.us-east-2.amazonaws.com:61617";  //prod broker 
            NMSConnectionFactory factory = new NMSConnectionFactory(brokerUri);
        
            using (IConnection connection = factory.CreateConnection("admin","adminactivemq"))
            {
                connection.Start();
        
                using (ISession session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge))
                using (IDestination dest = session.GetQueue(queueName))
                using (IMessageProducer producer = session.CreateProducer(dest))
                { 
                    producer.DeliveryMode = MsgDeliveryMode.NonPersistent;        
                    producer.Send(message);
                    Console.WriteLine($"Sent {message} messages");
                }
            }
        }

    }
}
