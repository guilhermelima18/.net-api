using CursoAPI.Models.Usuarios;

namespace CursoAPI.Configurations
{
    public interface IAuthenticationService
    {
        string GerarToken(UsuarioViewModelOutput usuarioViewModelOutput);
    }
}
