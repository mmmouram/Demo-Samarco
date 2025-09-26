import { obterDashboard, obterDetalhesCorreia } from '../service/dashboardService';

describe('dashboardService', () => {
  beforeEach(() => {
    global.fetch = jest.fn();
  });

  afterEach(() => {
    jest.resetAllMocks();
  });

  it('obterDashboard deve retornar dados em caso de sucesso', async () => {
    const fakeData = [{ id: 1, nome: 'Correia 1' }];
    global.fetch.mockResolvedValue({
      ok: true,
      json: jest.fn().mockResolvedValue(fakeData)
    });

    const result = await obterDashboard('status', '2023-10-10');
    expect(global.fetch).toHaveBeenCalledWith(expect.stringContaining('/api/Dashboard'));
    expect(result).toEqual(fakeData);
  });

  it('obterDashboard deve lançar erro em caso de falha', async () => {
    global.fetch.mockResolvedValue({ ok: false });

    await expect(obterDashboard('status', '2023-10-10')).rejects.toThrow('Erro ao obter dashboard');
  });

  it('obterDetalhesCorreia deve retornar dados em caso de sucesso', async () => {
    const fakeData = { id: 1, nome: 'Correia 1' };
    global.fetch.mockResolvedValue({
      ok: true,
      json: jest.fn().mockResolvedValue(fakeData)
    });

    const result = await obterDetalhesCorreia(1);
    expect(global.fetch).toHaveBeenCalledWith('/api/Dashboard/1');
    expect(result).toEqual(fakeData);
  });

  it('obterDetalhesCorreia deve lançar erro em caso de falha', async () => {
    global.fetch.mockResolvedValue({ ok: false });
    
    await expect(obterDetalhesCorreia(1)).rejects.toThrow('Erro ao obter detalhes da correia');
  });
});
