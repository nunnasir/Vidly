using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
	public class CustomerController : Controller
	{

		private ApplicationDbContext _context;

		public CustomerController()
		{
			_context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		[Authorize(Roles = RoleName.CanManageMovies)]
		public ActionResult New()
		{
			var memberShipType = _context.MembershipTypes.ToList();

			var viewModel = new CustomerViewModel
			{
				Customer = new Customer(),
				MembershipTypes = memberShipType
			};

			return View("CustomerForm",viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Save(Customer customer)
		{
			if (!ModelState.IsValid)
			{
				var viewModel = new CustomerViewModel
				{
					Customer = customer,
					MembershipTypes	= _context.MembershipTypes.ToList()
				};
				return View("CustomerForm", viewModel);
			}

			if (customer.Id == 0)
				
				_context.Customers.Add(customer);
			else
			{
				var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
				//TryUpdateModel(customerInDb, "", new string[]{"Name", "Email"});
				//TryUpdateModel(customerInDb);
				customerInDb.Name = customer.Name;
				customerInDb.DateOfBirth = customer.DateOfBirth;
				customerInDb.IsSubscribedNewsLetter = customer.IsSubscribedNewsLetter;
				customerInDb.MembershipTypeId = customer.MembershipTypeId;
			}
			_context.SaveChanges();

			return RedirectToAction("Index","Customer");
		}

		//
		// GET: /Customer/
		public ViewResult Index()
		{
			if(User.IsInRole(RoleName.CanManageMovies))
				return View("List");
			return View("ReadOnlyList");
		}

		public ActionResult Details(int id)
		{
			var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

			if (customer == null)
			{
				return HttpNotFound();
			}

			return View(customer);
		}

		[Authorize(Roles = RoleName.CanManageMovies)]
		public ActionResult Edit(int id)
		{
			var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
			if (customer == null)
			{
				return HttpNotFound();
			}

			var viewModel = new CustomerViewModel
			{
				Customer = customer,
				MembershipTypes = _context.MembershipTypes.ToList()
			};
			return View("CustomerForm", viewModel);
		}
	}
}