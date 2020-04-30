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

namespace GTMS.Controllers
{
    public class MessageController : Controller
    {
        private readonly GtmsDbContext _context;

        public MessageController(GtmsDbContext context)
        {
            _context = context;
        }

        // GET: Message
        public async Task<IActionResult> Index()
        {
            return View(await _context.Messages.ToListAsync());
        }

        // GET: Message/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .FirstOrDefaultAsync(m => m.msgID == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: Message/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Message/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("msgID,Description")] GTMS.Models.Message message)
        {         
            SendQueueMessages(message);

             if (ModelState.IsValid){
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
             }
           return View(message);                           
        }        
 


        // POST: Message/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("msgID,Description")] GTMS.Models.Message message)
        {
            if (id != message.msgID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.msgID))
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
            return View(message);
        }

        // GET: Message/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .FirstOrDefaultAsync(m => m.msgID == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Message/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.msgID == id);
        }

        public void SendQueueMessages(GTMS.Models.Message message){

            // definir nombre del queue, en caso de no existir activemq lo crea
            // definir el uri del endpoint de aw - aws acepta ssl no tcp asi que el string cambia en el protocolo
            // al ser ssl hay que enviar las credenciales
            // apache.nms connectionfactory provee manejo de comunicacion con el queue
            // definir el "producer" en caso de la clase que envia de mensaje 
            // definir el "receiver" en caso de ser el servicio consumiendo el queue 

            string queueName = "dev_queue"; 
            Console.WriteLine($"Adding message to queue topic: {queueName}");        
            string brokerUri = $"activemq:ssl://b-57e8bf3e-69c9-4bec-b528-de407901bd09-1.mq.us-east-2.amazonaws.com:61617";  // Default port
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
