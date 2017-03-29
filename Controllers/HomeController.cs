using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

using Models;

using Ex1.Data;
using Microsoft.AspNetCore.Identity;
using Ex1.Models;

namespace Ex1.Controllers
{
    public class HomeController : Controller
    {
        private GjesterDbContext dab;

        private ApplicationDbContext dbInnlegg;
        private UserManager<ApplicationUser> um;

        public HomeController(ApplicationDbContext dbInnlegg, GjesterDbContext dab, UserManager<ApplicationUser> um)
        {
            this.dbInnlegg = dbInnlegg;
            this.dab = dab;
            this.um = um;
        }

  
        
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        
        [HttpGet]
        public IActionResult ListGjester()
        {
            var vm = new Models.ListGjesterViewModel();

            vm.Gjest = new Models.Gjest();
            vm.Gjester = dab.Gjester.ToList();

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult ListGjester(Models.ListGjesterViewModel s)
        {
            var vm = new Models.ListGjesterViewModel();

            if (s.Gjest.gName == null || s.Gjest.txtTittel == null || s.Gjest.txtMessage == null)
            {
                vm.Gjest = new Models.Gjest();
                vm.Gjester = dab.Gjester.ToList();
            }
            else
            {
                dab.Gjester.Add(s.Gjest);
                dab.SaveChanges();
                ModelState.Clear();

                vm.Gjest = new Models.Gjest();
                vm.Gjester = dab.Gjester.ToList();                
            }

            return View(vm);
        }

        [HttpGet]
        public IActionResult EditInnlegg(int Id)
        {
            var vm = new Models.ListInnleggViewModel();

            vm.Innlegg = new Models.Innlegg();
            vm.Innlegg = dbInnlegg.Innlegger.First(x => x.Id == Id); 

            return View(vm);
        }

        [HttpPost]
        public IActionResult EditInnlegg(ListInnleggViewModel sendInnlegg)
        {              
            if (ModelState.IsValid)
            {
                var oldInnlegg = dbInnlegg.Innlegger.First(x => x.Id == sendInnlegg.Innlegg.Id); 
                oldInnlegg.txtTittel = sendInnlegg.Innlegg.txtTittel;
                oldInnlegg.txtMessage = sendInnlegg.Innlegg.txtMessage;
                dbInnlegg.SaveChanges();

                return RedirectToAction("Innlegg");      
            }

            return View(sendInnlegg);
        }
        
        [HttpGet]
        public IActionResult Innlegg()
        {
            var vm = new Models.ListInnleggViewModel();

            vm.Innlegg = new Models.Innlegg();
            vm.Innlegger = dbInnlegg.Innlegger.Include(i => i.User).ToList(); 

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Innlegg(Models.ListInnleggViewModel s)
        {
            var vm = new Models.ListInnleggViewModel();

            if (ModelState.IsValid)
            {
                s.Innlegg.User = um.GetUserAsync(User).Result;
                dbInnlegg.Innlegger.Add(s.Innlegg);
                dbInnlegg.SaveChanges();

                return RedirectToAction("Innlegg");      
            }

            return View(vm);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
