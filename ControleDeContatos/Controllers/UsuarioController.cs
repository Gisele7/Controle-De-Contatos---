using ControleDeContatos.Filters;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    [PaginaRestritaSomenteAdmin]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();

            return View(usuarios);
        }
        public IActionResult Criar()
        {
            return View();
        }
        public IActionResult ApagarConfirmacao(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _usuarioRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Usuário excluído com sucesso";
                }
                else
                {
                    TempData["MensagemErro"] = $"Ops, não conseguimos excluir seu usuário";
                }


                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos excluir seu usuário! Mais detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
        public IActionResult Editar(int id)
        {
            //Variavel
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        } 
        #region POSTS

        [HttpPost]
            public IActionResult Criar(UsuarioModel usuario)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        usuario = _usuarioRepositorio.Adicionar(usuario);
                        TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso";
                        return RedirectToAction("Index");
                    }

                    return View(usuario);
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu usuário, tente novamente! Detalhe do erro: {ex.Message}";
                    return RedirectToAction("Index");
                }

            }
        public IActionResult Alterar(UsuarioSemSenhaModel usuarioSemSenhaModel)
        {
            try
            {
                UsuarioModel usuario = null;
                if (ModelState.IsValid)
                {
                    usuario = new UsuarioModel()
                    {
                        Id = usuarioSemSenhaModel.Id,
                        Nome = usuarioSemSenhaModel.Nome,
                        Login =  usuarioSemSenhaModel.Login,
                        Email = usuarioSemSenhaModel.Email,
                        Perfil = usuarioSemSenhaModel.Perfil
                    };

                    usuario = _usuarioRepositorio.Atualizar(usuario);
                    TempData["MensagemSucesso"] = "Usuário alterado com sucesso";
                    return RedirectToAction("Index");
                }
                return View("Editar", usuario);
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos alterar seu usuário, tente novamente! Detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        #endregion
    }
    }
