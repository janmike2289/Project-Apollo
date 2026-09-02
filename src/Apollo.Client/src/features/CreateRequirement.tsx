import * as React from 'react';
import {
  Box,
  Button,
  Card,
  CardContent,
  FormControl,
  Grid,
  InputLabel,
  MenuItem,
  Select,
  Stack,
  TextField,
  Typography,
} from '@mui/material';
import CloudUploadIcon from '@mui/icons-material/CloudUpload';

export default function CreateRequirement() {
  const [priority, setPriority] = React.useState('Medium');
  const [category, setCategory] = React.useState('Feature');
  const [attachments, setAttachments] = React.useState<File[]>([]);
  const fileInputRef = React.useRef<HTMLInputElement>(null);

  const handleAttachmentClick = () => {
    fileInputRef.current?.click();
  };

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const files = event.target.files;
    if (files) {
      setAttachments([...attachments, ...Array.from(files)]);
    }
  };

  const handleRemoveAttachment = (index: number) => {
    setAttachments(attachments.filter((_, i) => i !== index));
  };

  return (
    <Card sx={{ background: '#e3f2fd', color: '#0f172a', border: '1px solid rgba(148, 163, 184, 0.2)' }}>
      <CardContent>
        <Typography variant="h5" sx={{ mb: 3 }}>
          Create New Requirement
        </Typography>

        <Box component="form" sx={{ display: 'grid', gap: 3 }}>
          <Grid container spacing={2}>
            <Grid size={{ xs: 12, md: 8 }}>
              <TextField
                fullWidth
                label="Name"
                placeholder="Enter a name for the new requirement"
                variant="outlined"
                sx={{ '& .MuiInputBase-root': { background: '#ffffff', color: '#0f172a' } }}
              />
            </Grid>

            <Grid size={{ xs: 12, md: 4 }}>
              <TextField
                fullWidth
                label="Requestor"
                placeholder="Name or team"
                variant="outlined"
                sx={{ '& .MuiInputBase-root': { background: '#ffffff', color: '#0f172a' } }}
              />
            </Grid>

            <Grid size={{ xs: 12, md: 4 }}>
              <FormControl fullWidth>
                <InputLabel>Priority</InputLabel>
                <Select
                  value={priority}
                  label="Priority"
                  onChange={(event) => setPriority(event.target.value)}
                  sx={{ background: '#ffffff' }}
                >
                  <MenuItem value="Low">Low</MenuItem>
                  <MenuItem value="Medium">Medium</MenuItem>
                  <MenuItem value="High">High</MenuItem>
                  <MenuItem value="Critical">Critical</MenuItem>
                </Select>
              </FormControl>
            </Grid>

            <Grid size={{ xs: 12, md: 4 }}>
              <FormControl fullWidth>
                <InputLabel>Category</InputLabel>
                <Select
                  value={category}
                  label="Category"
                  onChange={(event) => setCategory(event.target.value)}
                  sx={{ background: '#ffffff' }}
                >
                  <MenuItem value="Feature">Feature</MenuItem>
                  <MenuItem value="Bug">Bug</MenuItem>
                  <MenuItem value="Enhancement">Enhancement</MenuItem>
                  <MenuItem value="Compliance">Compliance</MenuItem>
                </Select>
              </FormControl>
            </Grid>

            <Grid size={{ xs: 12, md: 4 }}>
              <TextField
                fullWidth
                label="Target Date"
                type="date"
                InputLabelProps={{ shrink: true }}
                sx={{ '& .MuiInputBase-root': { background: '#ffffff', color: '#0f172a' } }}
              />
            </Grid>
          </Grid>

          <TextField
            fullWidth
            label="Description"
            multiline
            minRows={6}
            placeholder="Describe the requirement, expected outcome, and business value"
            variant="outlined"
            sx={{ '& .MuiInputBase-root': { background: '#ffffff', color: '#0f172a' } }}
          />

          <Box sx={{ pt: 2 }}>
            <Typography variant="subtitle2" sx={{ mb: 2 }}>
              Attachments
            </Typography>
            <input
              ref={fileInputRef}
              type="file"
              multiple
              hidden
              onChange={handleFileChange}
            />
            <Button
              variant="outlined"
              startIcon={<CloudUploadIcon />}
              onClick={handleAttachmentClick}
              sx={{ mb: attachments.length > 0 ? 2 : 0 }}
            >
              Add Attachment
            </Button>
            {attachments.length > 0 && (
              <Box sx={{ display: 'flex', flexDirection: 'column', gap: 1 }}>
                {attachments.map((file, index) => (
                  <Box
                    key={index}
                    sx={{
                      display: 'flex',
                      justifyContent: 'space-between',
                      alignItems: 'center',
                      p: 1.5,
                      background: 'rgba(15, 23, 42, 0.05)',
                      border: '1px solid rgba(148, 163, 184, 0.2)',
                      borderRadius: 1,
                    }}
                  >
                    <Typography variant="body2">{file.name}</Typography>
                    <Button
                      size="small"
                      variant="text"
                      color="error"
                      onClick={() => handleRemoveAttachment(index)}
                    >
                      Remove
                    </Button>
                  </Box>
                ))}
              </Box>
            )}
          </Box>

          <Stack direction="row" spacing={2} justifyContent="flex-end">
            <Button variant="outlined" color="inherit">
              Cancel
            </Button>
            <Button variant="contained" color="primary">
              Save Requirement
            </Button>
          </Stack>
        </Box>
      </CardContent>
    </Card>
  );
}
