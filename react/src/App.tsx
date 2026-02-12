import { useDataJobs } from './hooks/useDataJobs'
import { DataJobList } from './features/dataProcessing/DataJobList';

function App() {
  const { jobs, startJob } = useDataJobs();

  return (
    <div style={{ padding: '2rem', fontFamily: 'Arial, sans-serif' }}>
      <h1>Milledesk Data Job Demo</h1>
      <button onClick={startJob} style={{ padding: '0.5rem 1rem', marginBottom: '1rem' }}>
        Start Data Job
      </button>

      <DataJobList jobs={jobs} />
    </div>
  );
}

export default App;
