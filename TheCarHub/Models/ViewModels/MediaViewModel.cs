using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace TheCarHub.Models.ViewModels
{
    public class MediaViewModel
    {
        public int Id { get; set; }
        public int ListingId { get; set; }
        public string FileName { get; set; }
        public string Caption { get; set; }
    }
}