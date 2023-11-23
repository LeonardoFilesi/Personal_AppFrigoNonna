using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppFrigoNonna.Models
{
    public class FridgeProdFormModel
    {
        public FridgeProd FridgeProd { get; set; }

        public IFormFile? FridgeProdFormFile { get; set; }

        // Serve per gestire "Select" che seleziona istanze multiple nelle viste (Multiple per la relazione N a N)
        public List<SelectListItem>? Category { get; set; }
        public List<string>? SelectedCategoryId { get; set; }
    }
}
