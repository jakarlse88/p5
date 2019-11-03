using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheCarHub.Models.Entities;

namespace TheCarHub.Models.ViewModels
{
    public class ListingViewModel
    {   
        [HiddenInput]
        public int Id { get; set; }
        
        [DisplayName("Listing Title")]
        public string Title { get; set; }
        
        public List<Media> Media { get; set; }

        [DisplayName("Media")]
        public List<IFormFile> FormFiles { get; set; }
        
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DisplayName("Tags")]
        public List<ListingTag> ListingTags { get; set; }
        
        public string Status { get; set; }

        [DisplayName("Price")]    
        public decimal SellingPrice { get; set; }
    }
}