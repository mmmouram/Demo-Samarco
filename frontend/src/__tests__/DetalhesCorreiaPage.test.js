import React from 'react';
import { render, screen, waitFor } from '@testing-library/react';
import DetalhesCorreiaPage from '../page/DetalhesCorreiaPage';
import * as dashboardService from '../service/dashboardService';
import { MemoryRouter, Route, Routes } from 'react-router-dom';

jest.mock('../service/dashboardService');

const fakeDetalhes = {
  id: 1,
  nome: 'Correia 1',
  risco: 85,
  dataUltimaInspecao: new Date().toISOString(),
  alerta: true
};

describe('DetalhesCorreiaPage', () => {
  it('deve renderizar os detalhes após carregar', async () => {
    dashboardService.obterDetalhesCorreia.mockResolvedValue(fakeDetalhes);
    render(
      <MemoryRouter initialEntries={['/detalhes/1']}>
        <Routes>
          <Route path="/detalhes/:id" element={<DetalhesCorreiaPage />} />
        </Routes>
      </MemoryRouter>
    );

    expect(screen.getByText(/Carregando.../i)).toBeInTheDocument();

    await waitFor(() => {
      expect(screen.getByText(/Detalhes da Correia/)).toBeInTheDocument();
      expect(screen.getByText(/Correia 1/)).toBeInTheDocument();
      expect(screen.getByText(/Risco:/)).toBeInTheDocument();
    });
  });

  it('deve exibir mensagem de erro se falhar em carregar os detalhes', async () => {
    dashboardService.obterDetalhesCorreia.mockRejectedValue(new Error('Erro'));
    render(
      <MemoryRouter initialEntries={['/detalhes/1']}>
        <Routes>
          <Route path="/detalhes/:id" element={<DetalhesCorreiaPage />} />
        </Routes>
      </MemoryRouter>
    );

    await waitFor(() => {
      expect(screen.getByText('Erro ao carregar os detalhes da correia.')).toBeInTheDocument();
    });
  });
});
