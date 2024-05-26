using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DTO
{
    public class PublicationUserListDto
    {
        public List<PublicationUserDto> Publications { get; } = new List<PublicationUserDto>();
    }

    public class PublicationUserDto
    {
        // Add any necessary properties for a DTOPublicacionUsuario object
    }
}
