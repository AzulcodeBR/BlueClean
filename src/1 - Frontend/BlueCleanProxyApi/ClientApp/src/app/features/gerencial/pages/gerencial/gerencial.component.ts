import { ChangeDetectionStrategy, Component, computed, inject } from '@angular/core';
import { StringResources } from '../../../../core/constants/string-resources';
import { SessionService } from '../../../../core/services/session.service';

@Component({
  selector: 'app-gerencial',
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './gerencial.component.html',
  styleUrls: ['./gerencial.component.scss']
})
export class GerencialComponent {
  private readonly sessionService = inject(SessionService);

  protected readonly titulo = StringResources.GerencialTitulo;
  protected readonly usuarioLabel = StringResources.UsuarioAutenticadoLabel;
  protected readonly nomeUsuario = computed(() => this.sessionService.nomeUsuario() ?? '-');
}
