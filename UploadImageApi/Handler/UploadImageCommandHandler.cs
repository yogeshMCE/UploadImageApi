using AutoMapper;
using MediatR;
using UploadImageApi.Command;
using UploadImageApi.Models.Domain;
using UploadImageApi.Models.DTOs;
using UploadImageApi.Repository;

namespace UploadImageApi.Handler
{
    public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, ImageDto>
    {
        private readonly IImageRepository imageRepository;
        private readonly IMapper mapper;

        public UploadImageCommandHandler(IImageRepository imageRepository, IMapper mapper)
        {
            this.imageRepository = imageRepository;
            this.mapper = mapper;
        }
        public  async Task<ImageDto> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            // convert DTO to DomainModel
            var ImageDomainModel = new Image
            {
                File = request.File,
                FileExtention = Path.GetExtension(request.File.FileName),
                FileSizeInBytes = request.File.Length,
                FileName = request.FileName,
                FileDescription = request.FileDescription,
            };
            await imageRepository.UploadImageAsync(ImageDomainModel);

            return mapper.Map<ImageDto>(ImageDomainModel);
        }
        
    }
}
