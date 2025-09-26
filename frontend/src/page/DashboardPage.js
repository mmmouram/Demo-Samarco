import React, { useState, useEffect } from 'react';
import { obterDashboard } from '../service/dashboardService';
import ItemCorreia from '../component/ItemCorreia';
import './DashboardPage.css';

function DashboardPage() {
  const [filtroStatus, setFiltroStatus] = useState('');
  const [filtroDataInspecao, setFiltroDataInspecao] = useState('');
  const [correias, setCorreias] = useState([]);

  useEffect(() => {
    carregarDashboard();
    // eslint-disable-next-line
  }, []);

  const carregarDashboard = async () => {
    try {
      const dados = await obterDashboard(filtroStatus, filtroDataInspecao);
      // Destacar correias com maior risco em ordem decrescente
      const ordenados = dados.sort((a, b) => b.risco - a.risco);
      setCorreias(ordenados);
    } catch (error) {
      console.error('Erro ao carregar o dashboard:', error);
    }
  };

  const handleFiltro = async (e) => {
    e.preventDefault();
    await carregarDashboard();
  };

  const limparFiltros = () => {
    setFiltroStatus('');
    setFiltroDataInspecao('');
    carregarDashboard();
  };

  return (
    <div className="dashboard-container">
      <h1>Dashboard de Correias</h1>
      <form className="form-filtro" onSubmit={handleFiltro}>
        <input
          type="text"
          placeholder="Status de Risco"
          value={filtroStatus}
          onChange={(e) => setFiltroStatus(e.target.value)}
        />
        <input
          type="date"
          value={filtroDataInspecao}
          onChange={(e) => setFiltroDataInspecao(e.target.value)}
        />
        <button type="submit">Filtrar</button>
        <button type="button" onClick={limparFiltros}>Limpar</button>
      </form>
      <div className="lista-correias">
        {correias.map((correia) => (
          <ItemCorreia key={correia.id} correia={correia} />
        ))}
      </div>
    </div>
  );
}

export default DashboardPage;
