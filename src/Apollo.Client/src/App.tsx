import * as React from 'react';
import { Box, Typography } from '@mui/material';
import Dashboard from './features/Dashboard';
import CreateRequirement from './features/CreateRequirement';
import EditRequirement from './features/EditRequirement';
import RequirementsList from './features/RequirementsList';
import RmAppBar from './rm-appbar';
import RmSidebar, { type ViewKey } from './rm-sidebar';


const App = () => {
  const [open, setOpen] = React.useState(false);
  const [selectedView, setSelectedView] = React.useState<ViewKey>('dashboard');
  const [selectedRequirementId, setSelectedRequirementId] = React.useState<string | null>(null);

  const selectView = (view: ViewKey, requirementId?: string) => {
    setSelectedView(view);
    setSelectedRequirementId(requirementId ?? null);
    setOpen(false);
  };

  const renderSelectedView = () => {
    const viewMap: Record<ViewKey, React.ReactNode> = {
      dashboard: <Dashboard />,
      createRequirement: <CreateRequirement />,
      requirementsList: <RequirementsList onEdit={(id) => selectView('editRequirement', id)} />,
      editRequirement: (
        <EditRequirement
          requirementId={selectedRequirementId ?? 'REQ-1042'}
          onBack={() => selectView('requirementsList')}
        />
      ),
      reports: (
        <Box sx={{ p: 4, color: '#e2e8f0' }}>
          <Typography variant="h4">Reports</Typography>
        </Box>
      ),
      settings: (
        <Box sx={{ p: 4, color: '#e2e8f0' }}>
          <Typography variant="h4">Settings</Typography>
        </Box>
      ),
    };

    return viewMap[selectedView];
  };

  return (
    <Box sx={{ minHeight: '100vh', background: 'linear-gradient(180deg, #f8fafc 0%, #eef2f7 100%)' }}>
      <RmAppBar onOpenDrawer={() => setOpen(true)} />

      <RmSidebar
        open={open}
        selectedView={selectedView}
        onSelect={selectView}
        onClose={() => setOpen(false)}
      />

      <Box component="main" sx={{ p: 4, color: '#0f172a' }}>
        {renderSelectedView()}
      </Box>
    </Box>
  );
};

export default App;
