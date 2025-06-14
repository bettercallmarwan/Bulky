using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers;

public class CategoryController(IUnitOfWork unitOfWork) : Controller
{
    public IActionResult Index()
    {
        List<Category> objCategoryList = unitOfWork.CategoryRepository.GetAll().ToList();
        return View(objCategoryList);
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Category obj)
    {
        if (obj.DisplayOrder.ToString() == obj.Name)
        {
            ModelState.AddModelError("name", "The Display Order Cannot Exactly Match The Name");
        }

        if (ModelState.IsValid)
        {
            unitOfWork.CategoryRepository.Add(obj);
            unitOfWork.Save();
            TempData["success"] = "Category Created Successfully";
            return RedirectToAction("Index");
        }

        return View();
    }

    public IActionResult Edit([FromRoute]int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        Category? categoryFromDb = unitOfWork.CategoryRepository.Get(C => C.Id == id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }
        return View(categoryFromDb);
    }

    [HttpPost]
    public IActionResult Edit(Category obj)
    {
        if (ModelState.IsValid)
        {
            unitOfWork.CategoryRepository.Update(obj);
            unitOfWork.Save();
            TempData["success"] = "Category Edited Successfully";
            return RedirectToAction("Index");
        }
        return View();
    }
    
    public IActionResult Delete([FromRoute]int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        Category? categoryFromDb = unitOfWork.CategoryRepository.Get(C => C.Id == id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }
        return View(categoryFromDb);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Category? obj = unitOfWork.CategoryRepository.Get(C => C.Id == id);
        if (obj == null)
        {
            return NotFound();                                                                                                                                                                                                                                                                                                                                                  
        }

        unitOfWork.CategoryRepository.Remove(obj);
        unitOfWork.Save();
        TempData["success"] = "Category Deleted Successfully";
        return RedirectToAction("Index");
    }

}