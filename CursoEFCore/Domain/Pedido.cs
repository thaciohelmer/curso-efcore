﻿using CursoEFCore.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoEFCore.Domain
{
    public class Pedido
    {
        public int Id { get; set; }
        public int ClienteID { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime IniciadoEm { get; set; }
        public DateTime FinalizadoEm { get; set; }
        public TipoFrete TipoFrete { get; set; }
        public StatusPedido Status { get; set; }
        public string Observacao { get; set; }
        public ICollection<PedidoItem> Itens { get; set; }
    }
}
