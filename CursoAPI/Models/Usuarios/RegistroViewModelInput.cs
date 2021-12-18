﻿using System.ComponentModel.DataAnnotations;

namespace CursoAPI.Models.Usuarios
{
    public class RegistroViewModelInput
    {
        [Required(ErrorMessage = "O login é obrigatório.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Senha { get; set; }
    }
}