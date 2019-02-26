using Microsoft.AspNetCore.Mvc;
using System;
using AnimalShelter.Models;
using System.Collections.Generic;

namespace AnimalShelter.Controllers
{
  public class AnimalController : Controller
  {

    [HttpGet("/animal")]
    public ActionResult Index()
    {
      return View(Animal.GetAll(""));
    }

    [HttpGet("/animal/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/animal")]
    public ActionResult Create(string name, string sex, string breed, DateTime dateOfAdmittance)
    {
      Animal newAnimal = new Animal(name,sex,breed,dateOfAdmittance);
      newAnimal.Save();
      return RedirectToAction("Index");
    }

    [HttpGet("/animal/{sort}")]
    public ActionResult Index2(string sort)
    {
      List<Animal> TestAnimal = Animal.GetAll(sort);
      return View("Index", TestAnimal);
    }
  }
}
