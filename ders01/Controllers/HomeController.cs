﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ders01.Models;
using ders01.Models.Entities;

namespace ders01.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    MydbContext db = new MydbContext();

    public IActionResult Index()
    {
        return View();
    }
    [Route("/privacy")]

    public IActionResult Privacy()
    {
        return View();
    }

    [Route("/contact")]

    public IActionResult Contact()
    {
        return View();
    }
    [Route("/todolist")]
    public IActionResult TodoList()
    {
        var model = new TodoViewModel()
        {
            Todos = db.Todos.OrderByDescending(x => x.Id).ToList()
        };
        return View(model);
    }
    [HttpPost]
    [IgnoreAntiforgeryToken]
    [Route("/add-todo")]
    public IActionResult AddTodo(Todo postedData)
    {
        Todo toAdd = new Todo();
        toAdd.Title = postedData.Title;
        toAdd.IsComplated = false;
        db.Add(toAdd);
        db.SaveChanges();
        return Redirect("/todolist");
    }

    [Route("/remove-todo/{id}")]
    public IActionResult RemoveTodo(int id)
    {
        Todo toDelete = db.Todos.Find(id)!;
        db.Remove(toDelete);
        db.SaveChanges();
        return Redirect("/todolist");
    }
    [Route("/update-todo/{id}")]
    public IActionResult UpdateTodo(int id)
    {
        Todo toUpdate = db.Todos.Find(id)!;
        toUpdate.IsComplated = !toUpdate.IsComplated;
        db.Entry(toUpdate).CurrentValues.SetValues(toUpdate);
        db.SaveChanges();
        return Content(toUpdate.IsComplated.ToString()!);
    }
    [Route("/ortalamatik")]

    public IActionResult OrtalaMatik()
    {
        var model = new LessonViewModel()
        {
            Lessons = db.Derslers.OrderByDescending(x => x.Id).ToList()
        };
        return View(model);
    }
    [HttpPost]
    [IgnoreAntiforgeryToken]
    [Route("/add-lesson")]
    public IActionResult AddLesson(Dersler postedData)
    {
        Dersler leAdd = new Dersler();
        leAdd.Title = postedData.Title!;
        leAdd.Kredi = postedData.Kredi!;
        leAdd.Note = postedData.Note!;

        db.Add(leAdd);
        db.SaveChanges();
        return Redirect("/ortalamatik");
    }
    [Route("/remove-lesson/{id}")]
    public IActionResult RemoveLesson(int id)
    {
        Dersler toDelete = db.Derslers.Find(id)!;
        db.Remove(toDelete);
        db.SaveChanges();
        return Redirect("/ortalamatik");
    }
    
    [Route("/update-lesson/{id}")]
    public IActionResult UpdateLesson(int id, Dersler postData)
    {
        Dersler toUpdate = db.Derslers.Find(id)!;
        toUpdate.Title = postData.Title;
        toUpdate.Kredi = postData.Kredi;
        toUpdate.Note = postData.Note;
        db.Entry(toUpdate).CurrentValues.SetValues(toUpdate);
        db.SaveChanges();
        return Redirect("/ortalamatik");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
