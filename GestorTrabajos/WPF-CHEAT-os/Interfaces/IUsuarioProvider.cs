using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_CHEAT_os.DTO;

namespace WPF_CHEAT_os.Interfaces
{
    public interface IUsuarioProvider
    {
        Task<IEnumerable<UsuarioDTO>> GetUsuarioDTOAsync();
        Task<IEnumerable<GetUsuarioDTO>> GetGetUsuarioDTOAsync();
    }
}
