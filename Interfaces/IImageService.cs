using System;
using CloudinaryDotNet.Actions;

namespace _netstore.Interfaces
{
	public interface IImageService
	{
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);

        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}

