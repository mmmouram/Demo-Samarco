import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import DashboardPage from '../page/DashboardPage';
import DetalhesCorreiaPage from '../page/DetalhesCorreiaPage';

function Rotas() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<DashboardPage />} />
        <Route path="/detalhes/:id" element={<DetalhesCorreiaPage />} />
      </Routes>
    </Router>
  );
}

export default Rotas;
