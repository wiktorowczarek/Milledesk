import axios from 'axios';

const api = axios.create({
  baseURL: '/api',
  withCredentials: true,
});

export const startJob = async (): Promise<string> => {
  const response = await api.post('/data');
  return response.data.jobId;
};

export const getJobStatus = async (jobId: string) => {
  const response = await api.get(`/data/${jobId}`);
  return response.data;
};