import React from 'react';
import { useNavigate } from 'react-router-dom';
import './ItemCorreia.css';

function ItemCorreia({ correia }) {
  const navigate = useNavigate();

  const handleClick = () => {
    navigate(`/detalhes/${correia.id}`);
  };

  return (
    <div className={`card-correia ${correia.alerta ? 'alerta' : ''}`} onClick={handleClick}>
      <h2>{correia.nome}</h2>
      <p>Risco: {correia.risco}</p>
      <p>Data da Última Inspeção: {new Date(correia.dataUltimaInspecao).toLocaleDateString()}</p>
      {correia.alerta && <span className="badge-alerta">ALERTA CRÍTICO</span>}
    </div>
  );
}

export default ItemCorreia;
