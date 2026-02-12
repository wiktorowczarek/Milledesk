import { useState, useEffect, useCallback } from 'react';
import { startJob as apiStartJob, getJobStatus as apiGetJobStatus } from '../api/dataApi';
import { getJobId, setJobId, clearJobId } from '../utils/storage';

export const useDataJob = () => {
  const [jobId, setJobIdState] = useState<string | null>(getJobId());
  const [status, setStatus] = useState<'Processing' | 'Completed' | 'Failed' | null>(null);
  const [data, setData] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [processing, setProcessing] = useState(false);
  const [elapsed, setElapsed] = useState(0);

  const start = useCallback(async () => {
    clearJobId();
    setStatus('Processing');
    setProcessing(true);
    setElapsed(0);
    setData(null);
    setError(null);

    const newJobId = await apiStartJob();
    setJobId(newJobId);
    setJobIdState(newJobId);
    setJobId(newJobId);
  }, []);

  useEffect(() => {
    if (!processing) return;
    const timer = setInterval(() => setElapsed((prev) => prev + 1), 1000);
    return () => clearInterval(timer);
  }, [processing]);

  useEffect(() => {
    if (!jobId) return;

    let interval: NodeJS.Timer;

    const poll = async () => {
      try {
        const res = await apiGetJobStatus(jobId);
        if (res.status === 'Processing') {
          setStatus('Processing');
        } else if (res.status === 'Completed') {
          setStatus('Completed');
          setData(res.data);
          setProcessing(false);
          clearInterval(interval);
        } else if (res.status === 'Failed') {
          setStatus('Failed');
          setError(res.error || 'Unknown error');
          setProcessing(false);
          clearInterval(interval);
        }
      } catch (err) {
        console.error(err);
      }
    };

    poll();
    interval = setInterval(poll, 5000);

    return () => clearInterval(interval);
  }, [jobId]);

  return { jobId, status, data, error, processing, elapsed, start };
};
