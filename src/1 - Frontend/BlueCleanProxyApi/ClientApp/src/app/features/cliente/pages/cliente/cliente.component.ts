import { ChangeDetectionStrategy, Component, computed, inject } from '@angular/core';
import { StringResources } from '../../../../core/constants/string-resources';
import { AuthSessionService } from '../../../../core/services/auth-session.service';

@Component({
  selector: 'app-cliente',
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.scss']
})
export class ClienteComponent {
  private readonly authSessionService = inject(AuthSessionService);

  protected readonly titulo = StringResources.ModuloClienteTitulo;
  protected readonly usuarioLabel = StringResources.UsuarioAutenticadoLabel;
  protected readonly nomeUsuario = computed(() => this.authSessionService.nomeUsuario() ?? '-');
}
