using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace TheCarHub.Models.ViewModels
{
    public class MediaViewModel
    {
        public int Id { get; set; }
        public int ListingForeignKey { get; set; }
        public string FileName { get; set; }
        public string Caption { get; set; }
        public List<FormFile> FormFiles { get; set; }
    }
}