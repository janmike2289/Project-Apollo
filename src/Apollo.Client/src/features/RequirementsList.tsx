import * as React from 'react';
import {
  Box,
  Chip,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Typography,
} from '@mui/material';

type RequirementsListProps = {
  onEdit: (id: string) => void;
};

const requirements = [
  { id: 'REQ-1042', title: 'Mobile Dashboard Refresh', owner: 'Maya Chen', status: 'In Review', priority: 'High', dueDate: '2026-09-10' },
  { id: 'REQ-1048', title: 'Audit Workflow Automation', owner: 'Sam Patel', status: 'Approved', priority: 'Medium', dueDate: '2026-09-15' },
  { id: 'REQ-1051', title: 'Customer Portal Login Redesign', owner: 'Ava Johnson', status: 'Open', priority: 'High', dueDate: '2026-09-21' },
  { id: 'REQ-1060', title: 'API Rate Limit Alerting', owner: 'Luis Gomez', status: 'At Risk', priority: 'Critical', dueDate: '2026-09-08' },
  { id: 'REQ-1064', title: 'SSO Support for External Partners', owner: 'Daniel Kim', status: 'Approved', priority: 'Medium', dueDate: '2026-09-28' },
];

const statusColor: Record<string, 'default' | 'warning' | 'success' | 'error'> = {
  Open: 'default',
  'In Review': 'warning',
  Approved: 'success',
  'At Risk': 'error',
};

export default function RequirementsList({ onEdit }: RequirementsListProps) {
  return (
    <Box sx={{ display: 'grid', gap: 3 }}>
      <Typography variant="h4" sx={{ color: '#e2e8f0' }}>
        List of Requirements
      </Typography>

      <TableContainer component={Paper} sx={{ background: '#e3f2fd', color: '#0f172a', border: '1px solid rgba(148, 163, 184, 0.2)' }}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell sx={{ color: '#0f172a', fontWeight: 700 }}>ID</TableCell>
              <TableCell sx={{ color: '#0f172a', fontWeight: 700 }}>Title</TableCell>
              <TableCell sx={{ color: '#0f172a', fontWeight: 700 }}>Owner</TableCell>
              <TableCell sx={{ color: '#0f172a', fontWeight: 700 }}>Priority</TableCell>
              <TableCell sx={{ color: '#0f172a', fontWeight: 700 }}>Status</TableCell>
              <TableCell sx={{ color: '#0f172a', fontWeight: 700 }}>Due Date</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {requirements.map((requirement) => (
              <TableRow
                key={requirement.id}
                hover
                onClick={() => onEdit(requirement.id)}
                sx={{ cursor: 'pointer', '&:hover': { backgroundColor: 'rgba(148, 163, 184, 0.08)' } }}
              >
                <TableCell sx={{ color: '#0f172a' }}>{requirement.id}</TableCell>
                <TableCell sx={{ color: '#0f172a' }}>{requirement.title}</TableCell>
                <TableCell sx={{ color: '#0f172a' }}>{requirement.owner}</TableCell>
                <TableCell sx={{ color: '#0f172a' }}>{requirement.priority}</TableCell>
                <TableCell>
                  <Chip
                    label={requirement.status}
                    color={statusColor[requirement.status] ?? 'default'}
                    size="small"
                    sx={{ fontWeight: 600 }}
                  />
                </TableCell>
                <TableCell sx={{ color: '#0f172a' }}>{requirement.dueDate}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </Box>
  );
}
