using System;
using System.Collections.Generic;

namespace BlueCleanApi.Models.BlueCleanDb;

public partial class StatusGateway
{
    public int StatusGatewayId { get; set; }

    public string Descricao { get; set; } = null!;

    public virtual ICollection<GatewayLocal> GatewayLocal { get; set; } = new List<GatewayLocal>();
}

