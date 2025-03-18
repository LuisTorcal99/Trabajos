using WPF_CHEAT_os.DTO;
using WPF_CHEAT_os.Interfaces;
using WPF_CHEAT_os.Utils;

namespace WPF_CHEAT_os.Services
{
    public class UsuarioService : IUsuarioProvider
    {
        private readonly IHttpsJsonClientProvider<UsuarioDTO> _httpsJsonClientProvider;

        private readonly IHttpsJsonClientProvider<GetUsuarioDTO> _httpsJsonClientProviderGet;

        public UsuarioService(IHttpsJsonClientProvider<UsuarioDTO> httpsJsonClientProvider, IHttpsJsonClientProvider<GetUsuarioDTO> httpsJsonClientProviderGet)
        {
            _httpsJsonClientProvider = httpsJsonClientProvider;
            _httpsJsonClientProviderGet = httpsJsonClientProviderGet;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetUsuarioDTOAsync()
        {
            return await _httpsJsonClientProvider.GetAsync(Constants.USERS_PATH);
        }

        public async Task<IEnumerable<GetUsuarioDTO>> GetGetUsuarioDTOAsync()
        {
            return await _httpsJsonClientProviderGet.GetAsync(Constants.USERS_PATH);
        }
    }
}