using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.BLL.DTOModels;
using WebChat.BLL.Services;
using WebChat.Domain.Models;

namespace WebChat.BLL.Interfaces
{
    public interface IUserService
    {
        Result<bool> RegistredUser(UserDTO userDTO);
        Result<bool> AvtorizeitUser(UserDTO userDTO);
        User SersUser(UserDTO userDTO);
        List<UserDTO> GetAllDTOUser();
    }
}
