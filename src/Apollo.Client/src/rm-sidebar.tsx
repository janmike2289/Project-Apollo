import * as React from 'react';
import {
  Box,
  Divider,
  Drawer,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  Typography,
} from '@mui/material';
import SupportAgentIcon from '@mui/icons-material/SupportAgent';
import LogoutIcon from '@mui/icons-material/Logout';
import DashboardIcon from '@mui/icons-material/Dashboard';
import AddIcon from '@mui/icons-material/Add';
import BarChartIcon from '@mui/icons-material/BarChart';
import SettingsIcon from '@mui/icons-material/Settings';
import ListAltIcon from '@mui/icons-material/ListAlt';

export type ViewKey = 'dashboard' | 'createRequirement' | 'requirementsList' | 'editRequirement' | 'reports' | 'settings';

type RmSidebarProps = {
  open: boolean;
  selectedView: ViewKey;
  onSelect: (view: ViewKey) => void;
  onClose: () => void;
};

const rmNavItems: { label: string; value: ViewKey; icon: React.ReactNode }[] = [
  { label: 'Dashboard', value: 'dashboard', icon: <DashboardIcon /> },
  { label: 'New Requirement', value: 'createRequirement', icon: <AddIcon /> },
  { label: 'List of Requirements', value: 'requirementsList', icon: <ListAltIcon /> },
  { label: 'Reports', value: 'reports', icon: <BarChartIcon /> },
  { label: 'Settings', value: 'settings', icon: <SettingsIcon /> },
];

const commonNavItems: { label: string; value: ViewKey; icon: React.ReactNode }[] = [
  { label: 'Need Help', value: 'dashboard', icon: <SupportAgentIcon /> },
  { label: 'Log Out', value: 'createRequirement', icon: <LogoutIcon /> },
];

export default function RmSidebar({ open, selectedView, onSelect, onClose }: RmSidebarProps) {
  return (
    <Drawer anchor="left" open={open} onClose={onClose}>
      <Box sx={{ width: 260 }} role="presentation">
        <Typography variant="h6" sx={{ px: 3, py: 2 }}>
          Requirement Master
        </Typography>

        <Divider />

        <List>
          {rmNavItems.map((item) => (
            <ListItem key={item.value} disablePadding>
              <ListItemButton selected={selectedView === item.value} onClick={() => onSelect(item.value)}>
                <ListItemIcon>{item.icon}</ListItemIcon>
                <ListItemText primary={item.label} />
              </ListItemButton>
            </ListItem>
          ))}
        </List>

        <Divider />

        <List>
          {commonNavItems.map((item) => (
            <ListItem key={item.value} disablePadding>
              <ListItemButton selected={selectedView === item.value} onClick={() => onSelect(item.value)}>
                <ListItemIcon>{item.icon}</ListItemIcon>
                <ListItemText primary={item.label} />
              </ListItemButton>
            </ListItem>
          ))}
        </List>

      </Box>
    </Drawer>
  );
}
