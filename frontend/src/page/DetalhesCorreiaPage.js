import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { obterDetalhesCorreia } from '../service/dashboardService';
import './DetalhesCorreiaPage.css';

function DetalhesCorreiaPage() {
  const { id } = useParams();
  const [detalhes, setDetalhes] = useState(null);
  const [erro, setErro] = useState('');

  useEffect(() => {
    carregarDetalhes();
    // eslint-disable-next-line
  }, []);

  const carregarDetalhes = async () => {
    try {
      const data = await obterDetalhesCorreia(id);
      setDetalhes(data);
    } catch (error) {
      setErro('Erro ao carregar os detalhes da correia.');
    }
  };

  if (erro) {
    return <div className="detalhes-container"><p>{erro}</p></div>;
  }

  if (!detalhes) {
    return <div className="detalhes-container"><p>Carregando...</p></div>;
  }

  return (
    <div className="detalhes-container">
      <h1>Detalhes da Correia: {detalhes.nome}</h1>
      <p><strong>Risco:</strong> {detalhes.risco}</p>
      <p><strong>Alerta:</strong> {detalhes.alerta ? 'Sim' : 'Não'}</p>
      <p><strong>Data da Última Inspeção:</strong> {new Date(detalhes.dataUltimaInspecao).toLocaleDateString()}</p>
      {/* Espaço reservado para exibir inspeções manuais e leituras de sensores */}
    </div>
  );
}

export default DetalhesCorreiaPage;
