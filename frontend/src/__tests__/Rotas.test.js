import React from 'react';
import { render, screen } from '@testing-library/react';
import Rotas from '../route/Rotas';
import { MemoryRouter } from 'react-router-dom';

// Mock dos componentes de página
jest.mock('../page/DashboardPage', () => () => <div>DashboardPage</div>);
jest.mock('../page/DetalhesCorreiaPage', () => () => <div>DetalhesCorreiaPage</div>);

describe('Rotas', () => {
  it('deve renderizar DashboardPage na rota "/"', () => {
    render(
      <MemoryRouter initialEntries={['/']}>
        <Rotas />
      </MemoryRouter>
    );
    
    expect(screen.getByText('DashboardPage')).toBeInTheDocument();
  });

  it('deve renderizar DetalhesCorreiaPage na rota "/detalhes/1"', () => {
    render(
      <MemoryRouter initialEntries={['/detalhes/1']}>
        <Rotas />
      </MemoryRouter>
    );
    
    expect(screen.getByText('DetalhesCorreiaPage')).toBeInTheDocument();
  });
});
