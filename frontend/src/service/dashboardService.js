export async function obterDashboard(status, dataInspecao) {
  let url = '/api/Dashboard';
  const params = new URLSearchParams();
  
  if (status) {
    params.append('status', status);
  }
  if (dataInspecao) {
    params.append('dataInspecao', dataInspecao);
  }
  
  if (params.toString()) {
    url += `?${params.toString()}`;
  }
  
  const response = await fetch(url);
  if (!response.ok) {
    throw new Error('Erro ao obter dashboard');
  }
  return response.json();
}

export async function obterDetalhesCorreia(id) {
  const url = `/api/Dashboard/${id}`;
  const response = await fetch(url);
  if (!response.ok) {
    throw new Error('Erro ao obter detalhes da correia');
  }
  return response.json();
}
