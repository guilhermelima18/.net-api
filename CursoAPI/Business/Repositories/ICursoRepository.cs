using CursoAPI.Business.Entities;

namespace CursoAPI.Business.Repositories
{
    public interface ICursoRepository
    {
        void Adicionar(Curso curso);
        void Commit();
        IList<Curso> ObterPorUsuario(int codigoUsuario);
    }
}
