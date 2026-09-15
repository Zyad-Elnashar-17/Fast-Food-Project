using FastFood.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace FastFood.Web.ViewModel
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;


       
    }
}












//public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
//public ICollection<Item> Items { get; set; } = new List<Item>();