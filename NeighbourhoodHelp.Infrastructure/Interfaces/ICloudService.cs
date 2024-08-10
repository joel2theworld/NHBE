using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeighbourhoodHelp.Infrastructure.Interfaces
{
    public interface ICloudService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<ImageUploadResult> UpdatePhotoAsync(string id, IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
        Task<RawUploadResult> AddDocumentAsync(IFormFile document);

    }
}
