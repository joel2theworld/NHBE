using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NeighbourhoodHelp.Infrastructure.Helpers;
using NeighbourhoodHelp.Infrastructure.Interfaces;

namespace NeighbourhoodHelp.Infrastructure.Services
{
    public class CloudService : ICloudService
    {
        private readonly Cloudinary _cloudinary;
        public CloudService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account
            (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            
            _cloudinary = new Cloudinary ( acc );
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult(); //method for uploading images
            if (file.Length > 0) //Checks if there is at least 1 file
            {
                using var stream = file.OpenReadStream(); //Reads the file

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream), //Grabs the name of the file that is uploaded//transforms the image
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);  //uploads the file to cloudinary
            }

            return uploadResult;
        }
        public async Task<ImageUploadResult> UpdatePhotoAsync(string id, IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    PublicId = id// specify the public ID of the existing image to be replaced
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);

            return await _cloudinary.DestroyAsync(deletionParams);
        }

        public async Task<RawUploadResult> AddDocumentAsync(IFormFile doc)
        {
            var uploadResult = new RawUploadResult(); //method for uploading document
            if (doc.Length > 0) //Checks if there is at least 1 file
            {
                using var stream = doc.OpenReadStream(); //Reads the file

                var uploadParams = new RawUploadParams
                {
                    File = new FileDescription(doc.FileName, stream),
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);  //uploads the file to cloudinary
            }

            return uploadResult;
        }
    }
}
