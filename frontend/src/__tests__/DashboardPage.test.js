import React from 'react';
import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import DashboardPage from '../page/DashboardPage';
import * as dashboardService from '../service/dashboardService';

jest.mock('../service/dashboardService');

const fakeDashboardData = [
  { id: 1, nome: 'Correia 1', risco: 80, dataUltimaInspecao: new Date().toISOString(), alerta: true },
  { id: 2, nome: 'Correia 2', risco: 50, dataUltimaInspecao: new Date().toISOString(), alerta: false },
  { id: 3, nome: 'Correia 3', risco: 90, dataUltimaInspecao: new Date().toISOString(), alerta: true }
];

describe('DashboardPage', () => {
  beforeEach(() => {
    dashboardService.obterDashboard.mockResolvedValue(fakeDashboardData);
  });

  it('deve renderizar as correias em ordem decrescente de risco', async () => {
    render(<DashboardPage />);

    await waitFor(() => {
      // os itens devem ser ordenados: Correia 3 (risco 90) primeiro
      const items = screen.getAllByText(/Risco:/);
      expect(items[0]).toHaveTextContent('Risco: 90');
      expect(items[1]).toHaveTextContent('Risco: 80');
      expect(items[2]).toHaveTextContent('Risco: 50');
    });
  });

  it('deve aplicar os filtros e recarregar o dashboard', async () => {
    render(<DashboardPage />);
    
    const statusInput = screen.getByPlaceholderText('Status de Risco');
    const dateInput = screen.getByPlaceholderText(/Status de Risco/) ? screen.getByDisplayValue("") : screen.getByLabelText('');
    const filterButton = screen.getByText('Filtrar');

    fireEvent.change(statusInput, { target: { value: 'alto' } });
    fireEvent.change(dateInput, { target: { value: '2023-10-10' } });
    fireEvent.click(filterButton);
    
    await waitFor(() => {
      expect(dashboardService.obterDashboard).toHaveBeenCalledWith('alto', '2023-10-10');
    });
  });

  it('deve limpar os filtros e recarregar o dashboard', async () => {
    render(<DashboardPage />);
    
    const statusInput = screen.getByPlaceholderText('Status de Risco');
    fireEvent.change(statusInput, { target: { value: 'medio' } });

    const clearButton = screen.getByText('Limpar');
    fireEvent.click(clearButton);

    await waitFor(() => {
      expect(statusInput.value).toBe('');
      expect(dashboardService.obterDashboard).toHaveBeenCalledWith('', '');
    });
  });
});
