using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace TheCarHub.Models.InputModels
{
    public class MediaInputModel
    {
        public MediaInputModel()
        {
            Files = new List<FormFile>();
        }

        public IList<FormFile> Files { get; set; }
    }
}