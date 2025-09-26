import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import ItemCorreia from '../component/ItemCorreia';
import { MemoryRouter, Routes, Route } from 'react-router-dom';

// Componente dummy para simular a página de detalhes
function DummyDetalhes() {
  return <div>Detalhes da Correia</div>;
}

describe('ItemCorreia', () => {
  const correia = { id: 1, nome: 'Correia Teste', risco: 70, dataUltimaInspecao: new Date().toISOString(), alerta: false };

  it('deve navegar para DetalhesCorreiaPage ao clicar', () => {
    render(
      <MemoryRouter initialEntries={['/']}>
        <Routes>
          <Route path="/" element={<ItemCorreia correia={correia} />} />
          <Route path="/detalhes/:id" element={<DummyDetalhes />} />
        </Routes>
      </MemoryRouter>
    );
    
    fireEvent.click(screen.getByText('Correia Teste'));
    
    expect(screen.getByText('Detalhes da Correia')).toBeInTheDocument();
  });

  it('deve exibir o alerta crítico se correia.alerta for true', () => {
    const correiaAlerta = { ...correia, alerta: true };
    render(
      <MemoryRouter>
        <ItemCorreia correia={correiaAlerta} />
      </MemoryRouter>
    );

    expect(screen.getByText('ALERTA CRÍTICO')).toBeInTheDocument();
  });
});
