using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DogGo.Controllers
{
    public class OwnersController : Controller
    {
        // GET: Owners
        public ActionResult Index()
        {
            List<Owner> owners = _ownerRepo.GetAllOwners();

            return View(owners);
        }

        // GET: Owners/Details/5
        public ActionResult Details(int id)
        {
            Owner owner = _ownerRepo.GetOwnerById(id);
            List<Dog> dogs = _dogRepo.GetDogsByOwnerId(owner.Id);
            List<Walker> walkers = _walkerRepo.GetWalkersInNeighborhood(owner.NeighborhoodId);

            ProfileViewModel vm = new ProfileViewModel()
            {
                Owner = owner,
                Dogs = dogs,
                Walkers = walkers
            };

            return View(vm);
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            List<Neighborhood> neighborhoods = _neighborhoodRepo.GetAll();

            OwnerFormViewModel vm = new OwnerFormViewModel()
            {
                Owner = new Owner(),
                Neighborhoods = neighborhoods
            };

            return View(vm);
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OwnerFormViewModel vm)
        {
            try
            {
                _ownerRepo.AddOwner(vm.Owner);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(vm);
            }
        }

        // GET: HomeController1/Edit/5
        public ActionResult Edit(int id)
        {
            Owner owner = _ownerRepo.GetOwnerById(id);
            List<Neighborhood> neighborhoods = _neighborhoodRepo.GetAll();

            OwnerFormViewModel vm = new OwnerFormViewModel()
            {
                Owner = owner,
                Neighborhoods = neighborhoods
            };

            return View(vm);
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, OwnerFormViewModel vm)
        {
            try
            {
                _ownerRepo.UpdateOwner(vm.Owner);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(vm);
            }
        }

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            Owner owner = _ownerRepo.GetOwnerById(id);

            return View(owner);
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Owner owner)
        {
            try
            {
                _ownerRepo.DeleteOwner(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(owner);
            }
        }


        private readonly IOwnerRepository _ownerRepo;
        private readonly IDogRepository _dogRepo;
        private readonly IWalkerRepository _walkerRepo;
        private readonly INeighborhoodRepository _neighborhoodRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public OwnersController(IOwnerRepository ownerRepository, IDogRepository dogRepository, IWalkerRepository walkerRepository, INeighborhoodRepository neighborhoodRepo)
        {
            _ownerRepo = ownerRepository;
            _dogRepo = dogRepository;
            _walkerRepo = walkerRepository;
            _neighborhoodRepo = neighborhoodRepo;
        }
    }
}