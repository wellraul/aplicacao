﻿using System;

namespace ObjetoTransferencia
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Sexo { get; set; }
        public decimal LimiteCompra { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}
