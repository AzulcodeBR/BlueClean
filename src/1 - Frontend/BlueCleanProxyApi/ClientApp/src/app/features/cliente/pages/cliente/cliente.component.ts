import { ChangeDetectionStrategy, Component, computed, inject } from '@angular/core';
import { StringResources } from '../../../../core/constants/string-resources';
import { SessionService } from '../../../../core/services/session.service';

@Component({
  selector: 'app-cliente',
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.scss']
})
export class ClienteComponent {
  private readonly sessionService = inject(SessionService);

  protected readonly titulo = StringResources.ModuloClienteTitulo;
  protected readonly usuarioLabel = StringResources.UsuarioAutenticadoLabel;
  protected readonly nomeUsuario = computed(() => this.sessionService.nomeUsuario() ?? '-');
}
