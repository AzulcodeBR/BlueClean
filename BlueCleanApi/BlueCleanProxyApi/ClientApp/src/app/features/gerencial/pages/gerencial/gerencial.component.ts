import { ChangeDetectionStrategy, Component, computed, inject } from '@angular/core';
import { StringResources } from '../../../../core/constants/string-resources';
import { AuthSessionService } from '../../../../core/services/auth-session.service';

@Component({
  selector: 'app-gerencial',
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './gerencial.component.html',
  styleUrls: ['./gerencial.component.scss']
})
export class GerencialComponent {
  private readonly authSessionService = inject(AuthSessionService);

  protected readonly titulo = StringResources.GerencialTitulo;
  protected readonly usuarioLabel = StringResources.UsuarioAutenticadoLabel;
  protected readonly nomeUsuario = computed(() => this.authSessionService.nomeUsuario() ?? '-');
}
