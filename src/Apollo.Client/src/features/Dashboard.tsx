import * as React from 'react';
import {
  Box,
  Card,
  CardContent,
  Chip,
  Grid,
  Stack,
  Typography,
} from '@mui/material';

const summaryStats = [
  { label: 'Open Requirements', value: '24', tone: 'primary' },
  { label: 'In Review', value: '8', tone: 'warning' },
  { label: 'Approved', value: '14', tone: 'success' },
  { label: 'At Risk', value: '3', tone: 'error' },
];

const recentRequirements = [
  { title: 'Mobile dashboard refresh', owner: 'Maya Chen', status: 'In Review' },
  { title: 'Audit workflow automation', owner: 'Sam Patel', status: 'Approved' },
  { title: 'Customer portal login redesign', owner: 'Ava Johnson', status: 'Open' },
  { title: 'API rate limit alerting', owner: 'Luis Gomez', status: 'At Risk' },
];

const chartData = [
  { label: 'Open', value: 40, color: '#64b5f6' },
  { label: 'In Review', value: 25, color: '#ffb74d' },
  { label: 'Approved', value: 25, color: '#81c784' },
  { label: 'At Risk', value: 10, color: '#e57373' },
];

const deliveryHealth = 76;

const toneMap = {
  primary: '#90caf9',
  warning: '#ffcc80',
  success: '#81c784',
  error: '#ef9a9a',
};

export default function Dashboard() {
  const total = chartData.reduce((sum, item) => sum + item.value, 0);
  const chartGradient = `conic-gradient(${chartData
    .map((item, index) => {
      const start = chartData
        .slice(0, index)
        .reduce((sum, current) => sum + current.value, 0);
      const end = start + item.value;
      return `${item.color} ${start / total * 100}% ${end / total * 100}%`;
    })
    .join(', ')})`;

  return (
    <Box sx={{ display: 'grid', gap: 3 }}>
      <Grid container spacing={3}>
        {summaryStats.map((stat) => (
          <Grid key={stat.label} size={{ xs: 12, sm: 6, md: 3 }}>
            <Card sx={{ height: '100%', background: '#e3f2fd', color: '#0f172a', border: '1px solid rgba(148, 163, 184, 0.2)' }}>
              <CardContent>
                <Typography variant="body2" sx={{ opacity: 0.8, mb: 1 }}>
                  {stat.label}
                </Typography>
                <Stack direction="row" alignItems="center" justifyContent="space-between">
                  <Typography variant="h4" fontWeight={700}>
                    {stat.value}
                  </Typography>
                  <Chip
                    label="Live"
                    size="small"
                    sx={{
                      backgroundColor: toneMap[stat.tone as keyof typeof toneMap],
                      color: '#0f172a',
                      fontWeight: 700,
                    }}
                  />
                </Stack>
              </CardContent>
            </Card>
          </Grid>
        ))}
      </Grid>

      <Grid container spacing={3}>
        <Grid size={{ xs: 12, md: 5 }}>
          <Card sx={{ background: '#e3f2fd', color: '#0f172a', border: '1px solid rgba(148, 163, 184, 0.2)', height: '100%' }}>
            <CardContent>
              <Typography variant="h5" sx={{ mb: 3 }}>
                Delivery Health
              </Typography>

              <Stack direction="row" alignItems="center" justifyContent="center">
                <Box sx={{ width: 220, height: 180, position: 'relative' }}>
                  <svg viewBox="0 0 200 120" width="100%" height="100%" preserveAspectRatio="none">
                    <path
                      d="M 20 100 A 80 80 0 0 1 180 100"
                      fill="none"
                      stroke="rgba(148, 163, 184, 0.35)"
                      strokeWidth="16"
                      strokeLinecap="round"
                    />
                    <path
                      d="M 20 100 A 80 80 0 0 1 180 100"
                      fill="none"
                      stroke="#4dabf7"
                      strokeWidth="16"
                      strokeLinecap="round"
                      strokeDasharray={Math.PI * 80}
                      strokeDashoffset={Math.PI * 80 * (1 - deliveryHealth / 100)}
                    />
                  </svg>

                  <Box
                    sx={{
                      position: 'absolute',
                      inset: 'auto 0 8px 0',
                      display: 'flex',
                      flexDirection: 'column',
                      alignItems: 'center',
                      justifyContent: 'center',
                    }}
                  >
                    <Typography variant="h4" fontWeight={700}>{deliveryHealth}%</Typography>
                    <Typography variant="caption" sx={{ opacity: 0.7 }}>On track</Typography>
                  </Box>
                </Box>
              </Stack>
            </CardContent>
          </Card>
        </Grid>

        <Grid size={{ xs: 12, md: 7 }}>
          <Card sx={{ background: '#e3f2fd', color: '#0f172a', border: '1px solid rgba(148, 163, 184, 0.2)', height: '100%' }}>
            <CardContent>
              <Typography variant="h5" sx={{ mb: 2 }}>
                Status Breakdown
              </Typography>

              <Stack spacing={2}>
                {chartData.map((item) => (
                  <Stack key={item.label} direction="row" alignItems="center" justifyContent="space-between">
                    <Stack direction="row" alignItems="center" spacing={1.5}>
                      <Box sx={{ width: 12, height: 12, backgroundColor: item.color, borderRadius: '50%' }} />
                      <Typography>{item.label}</Typography>
                    </Stack>

                    <Typography sx={{ fontWeight: 700 }}>{item.value}%</Typography>
                  </Stack>
                ))}
              </Stack>
            </CardContent>
          </Card>
        </Grid>
      </Grid>

      <Card sx={{ background: '#e3f2fd', color: '#0f172a', border: '1px solid rgba(148, 163, 184, 0.2)' }}>
        <CardContent>
          <Typography variant="h5" sx={{ mb: 2 }}>
            Recent Requirements
          </Typography>

          <Stack spacing={2}>
            {recentRequirements.map((item) => (
              <Box
                key={item.title}
                sx={{
                  display: 'flex',
                  justifyContent: 'space-between',
                  alignItems: 'center',
                  border: '1px solid rgba(148, 163, 184, 0.2)',
                  borderRadius: 2,
                  p: 2,
                  background: 'rgba(255, 255, 255, 0.5)',
                }}
              >
                <Box>
                  <Typography fontWeight={600} sx={{ color: '#0f172a' }}>{item.title}</Typography>
                  <Typography variant="body2" sx={{ opacity: 0.7, color: '#0f172a' }}>
                    Owner: {item.owner}
                  </Typography>
                </Box>

                <Chip
                  label={item.status}
                  size="small"
                  color={
                    item.status === 'Approved'
                      ? 'success'
                      : item.status === 'In Review'
                        ? 'warning'
                        : item.status === 'At Risk'
                          ? 'error'
                          : 'default'
                  }
                />
              </Box>
            ))}
          </Stack>
        </CardContent>
      </Card>
    </Box>
  );
}
