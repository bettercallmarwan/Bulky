using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories;

public class Index(ApplicationDbContext _db) : PageModel
{
    public List<Category> CategoryList { get; set; }
    
    public void OnGet()
    {
        CategoryList = _db.Categories.ToList();
    }
}