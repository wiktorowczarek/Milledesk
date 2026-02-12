import { useDataJob } from './hooks/useDataJob';
import { DataJobForm } from './features/dataProcessing/DataJobForm';
import { DataJobStatus } from './features/dataProcessing/DataJobStatus';

function App() {
  const job = useDataJob();

  return (
    <div style={{ padding: '2rem', fontFamily: 'Arial, sans-serif' }}>
      <h1>Milledesk Data Job Demo</h1>
      <DataJobForm {...job} />
      <DataJobStatus {...job} />
    </div>
  );
}

export default App;