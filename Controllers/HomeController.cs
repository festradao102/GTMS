using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GTMS.Models;
using Apache.NMS;
using Apache.NMS.Util;

namespace GTMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Messages()
        {
            return View();
        }

        #pragma warning disable 1998
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Messages(String message)
        {
            IConnectionFactory iconnfactory = new NMSConnectionFactory("tcp://localhost:61616");
            IConnection conn = iconnfactory.CreateConnection();

            if (ModelState.IsValid)
            {
                conn.Start();

                ISession session = conn.CreateSession();
                IDestination dest = SessionUtil.GetDestination(session, "dev_queue");
                IMessageProducer msgProducer = session.CreateProducer(dest);
                
                msgProducer.Send(message);
                session.Close();
                conn.Stop();

                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        

    }
}
