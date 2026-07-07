import { routes } from './app.routes';

describe('App routes', () => {
  it('should redirect root to gerencial login', () => {
    const rootRoute = routes.find((route) => route.path === '');

    expect(rootRoute).toBeTruthy();
    expect(rootRoute?.redirectTo).toBe('gerencial/login');
    expect(rootRoute?.pathMatch).toBe('full');
  });

  it('should map cliente login route to LoginClienteComponent', async () => {
    const clienteLoginRoute = routes.find((route) => route.path === 'cliente/login');

    expect(clienteLoginRoute).toBeTruthy();
    expect(clienteLoginRoute?.data?.['tipoLogin']).toBe(1);

    const loaded = await clienteLoginRoute?.loadComponent?.();
    expect(loaded).toBeTruthy();
    expect((loaded as { name?: string }).name).toContain('LoginClienteComponent');
  });

  it('should map gerencial login route to LoginGerencialComponent', async () => {
    const gerencialLoginRoute = routes.find((route) => route.path === 'gerencial/login');

    expect(gerencialLoginRoute).toBeTruthy();
    expect(gerencialLoginRoute?.data?.['tipoLogin']).toBe(2);

    const loaded = await gerencialLoginRoute?.loadComponent?.();
    expect(loaded).toBeTruthy();
    expect((loaded as { name?: string }).name).toContain('LoginGerencialComponent');
  });

  it('should keep compatibility redirects from old login routes', () => {
    const oldClienteRoute = routes.find((route) => route.path === 'login/cliente');
    const oldGerencialRoute = routes.find((route) => route.path === 'login/gerencial');

    expect(oldClienteRoute?.redirectTo).toBe('cliente/login');
    expect(oldClienteRoute?.pathMatch).toBe('full');

    expect(oldGerencialRoute?.redirectTo).toBe('gerencial/login');
    expect(oldGerencialRoute?.pathMatch).toBe('full');
  });

  it('should expose cliente/cadastro and redirect register to it', () => {
    const cadastroRoute = routes.find((route) => route.path === 'cliente/cadastro');
    const registerLegacyRoute = routes.find((route) => route.path === 'register');

    expect(cadastroRoute).toBeTruthy();
    expect(typeof cadastroRoute?.loadComponent).toBe('function');

    expect(registerLegacyRoute?.redirectTo).toBe('cliente/cadastro');
    expect(registerLegacyRoute?.pathMatch).toBe('full');
  });
});
