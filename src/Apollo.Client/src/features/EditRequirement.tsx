import * as React from 'react';
import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
  Box,
  Button,
  Card,
  CardContent,
  FormControl,
  Grid,
  InputLabel,
  MenuItem,
  Paper,
  Select,
  Stack,
  TextField,
  Typography,
} from '@mui/material';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import CloudUploadIcon from '@mui/icons-material/CloudUpload';

type EditRequirementProps = {
  requirementId: string;
  onBack: () => void;
};

const requirement = {
  id: 'REQ-1042',
  title: 'Mobile Dashboard Refresh',
  owner: 'Maya Chen',
  priority: 'High',
  category: 'Feature',
  dueDate: '2026-09-10',
  description:
    'Refresh the mobile dashboard experience to improve visibility into project status, approvals, and overall delivery metrics.',
};

const commentHistory = [
  {
    author: 'Maya Chen',
    date: 'Today, 09:24 AM',
    text: 'Updated the requirement scope and requested a review of the dashboard refresh timeline.',
  },
  {
    author: 'Alex Morgan',
    date: 'Yesterday, 04:15 PM',
    text: 'Added a note recommending a dependency check before final approval.',
  },
  {
    author: 'Priya Shah',
    date: 'Sep 1, 02:10 PM',
    text: 'Confirmed design assets were uploaded and are ready for the next review cycle.',
  },
  {
    author: 'Jordan Lee',
    date: 'Aug 30, 11:45 AM',
    text: 'Requested clarification around acceptance criteria before implementation begins.',
  },
  {
    author: 'Nina Patel',
    date: 'Aug 28, 03:05 PM',
    text: 'Shared stakeholder feedback that the request should prioritize mobile accessibility.',
  },
];

export default function EditRequirement({ requirementId, onBack }: EditRequirementProps) {
  const [attachments, setAttachments] = React.useState<File[]>([]);
  const [expandedSection, setExpandedSection] = React.useState<string | false>('details');
  const [commentPage, setCommentPage] = React.useState(1);
  const fileInputRef = React.useRef<HTMLInputElement>(null);
  const commentsPerPage = 2;
  const totalCommentPages = Math.ceil(commentHistory.length / commentsPerPage);
  const paginatedComments = commentHistory.slice(
    (commentPage - 1) * commentsPerPage,
    commentPage * commentsPerPage,
  );

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
        <Stack direction="row" justifyContent="space-between" alignItems="center" sx={{ mb: 3 }}>
          <Typography variant="h5">Edit Requirement</Typography>
          {/* <Button variant="outlined" color="inherit" onClick={onBack}>
            Back to List
          </Button> */}
        </Stack>

        <Typography variant="body2" sx={{ mb: 2, opacity: 0.75 }}>
          {requirementId}
        </Typography>

        <Box component="form" sx={{ display: 'grid', gap: 2 }}>
          <Accordion
            expanded={expandedSection === 'details'}
            onChange={(_, isExpanded) => setExpandedSection(isExpanded ? 'details' : false)}
            sx={{ background: 'rgba(255,255,255,0.35)', border: '1px solid rgba(148,163,184,0.25)', borderRadius: '12px !important', overflow: 'hidden' }}
          >
            <AccordionSummary expandIcon={<ExpandMoreIcon />}>
              <Typography variant="subtitle1" sx={{ fontWeight: 600 }}>
                Requirement Details
              </Typography>
            </AccordionSummary>
            <AccordionDetails>
              <Grid container spacing={2}>
                <Grid size={{ xs: 12, md: 8 }}>
                  <TextField
                    fullWidth
                    label="Requirement Title"
                    defaultValue={requirement.title}
                    variant="outlined"
                    sx={{ '& .MuiInputBase-root': { background: '#ffffff', color: '#0f172a' } }}
                  />
                </Grid>

                <Grid size={{ xs: 12, md: 4 }}>
                  <TextField
                    fullWidth
                    label="Requestor"
                    defaultValue={requirement.owner}
                    variant="outlined"
                    sx={{ '& .MuiInputBase-root': { background: '#ffffff', color: '#0f172a' } }}
                  />
                </Grid>

                <Grid size={{ xs: 12, md: 4 }}>
                  <FormControl fullWidth>
                    <InputLabel>Priority</InputLabel>
                    <Select defaultValue={requirement.priority} label="Priority" sx={{ background: '#ffffff' }}>
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
                    <Select defaultValue={requirement.category} label="Category" sx={{ background: '#ffffff' }}>
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
                    defaultValue={requirement.dueDate}
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
                defaultValue={requirement.description}
                variant="outlined"
                sx={{ mt: 2, '& .MuiInputBase-root': { background: '#ffffff', color: '#0f172a' } }}
              />
            </AccordionDetails>
          </Accordion>

          <Accordion
            expanded={expandedSection === 'attachments'}
            onChange={(_, isExpanded) => setExpandedSection(isExpanded ? 'attachments' : false)}
            sx={{ background: 'rgba(255,255,255,0.35)', border: '1px solid rgba(148,163,184,0.25)', borderRadius: '12px !important', overflow: 'hidden' }}
          >
            <AccordionSummary expandIcon={<ExpandMoreIcon />}>
              <Typography variant="subtitle1" sx={{ fontWeight: 600 }}>
                Attachments
              </Typography>
            </AccordionSummary>
            <AccordionDetails>
              <input ref={fileInputRef} type="file" multiple hidden onChange={handleFileChange} />
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
            </AccordionDetails>
          </Accordion>

          <Accordion
            expanded={expandedSection === 'comments'}
            onChange={(_, isExpanded) => setExpandedSection(isExpanded ? 'comments' : false)}
            sx={{ background: 'rgba(255,255,255,0.35)', border: '1px solid rgba(148,163,184,0.25)', borderRadius: '12px !important', overflow: 'hidden' }}
          >
            <AccordionSummary expandIcon={<ExpandMoreIcon />}>
              <Typography variant="subtitle1" sx={{ fontWeight: 600 }}>
                Comments & History
              </Typography>
            </AccordionSummary>
            <AccordionDetails>
              <TextField
                fullWidth
                multiline
                minRows={4}
                placeholder="Add a comment for this requirement update..."
                variant="outlined"
                sx={{ '& .MuiInputBase-root': { background: '#ffffff', color: '#0f172a' } }}
              />
              <Stack direction="row" justifyContent="flex-end" sx={{ mt: 1.5 }}>
                <Button variant="contained" color="primary">
                  Save Comment
                </Button>
              </Stack>

              <Paper
                elevation={0}
                sx={{
                  mt: 3,
                  p: 2,
                  background: 'rgba(255, 255, 255, 0.5)',
                  border: '1px solid rgba(148, 163, 184, 0.25)',
                  borderRadius: 2,
                }}
              >
                <Typography variant="subtitle2" sx={{ mb: 2 }}>
                  Comment History
                </Typography>

                <Stack spacing={2}>
                  {paginatedComments.map((comment, index) => (
                    <Box key={`${comment.author}-${comment.date}-${index}`}>
                      <Typography variant="caption" sx={{ color: '#475569', display: 'block', mb: 0.5 }}>
                        {comment.author} • {comment.date}
                      </Typography>
                      <Typography variant="body2" sx={{ color: '#0f172a' }}>
                        {comment.text}
                      </Typography>
                    </Box>
                  ))}
                </Stack>

                
              </Paper>

              <Stack direction="row" justifyContent="space-between" alignItems="center" sx={{ mt: 2 }}>
                  <Typography variant="caption" sx={{ color: '#475569' }}>
                    Page {commentPage} of {totalCommentPages}
                  </Typography>
                  <Stack direction="row" spacing={1}>
                    <Button
                      size="small"
                      variant="outlined"
                      disabled={commentPage === 1}
                      onClick={() => setCommentPage((page) => Math.max(1, page - 1))}
                    >
                      Previous
                    </Button>
                    <Button
                      size="small"
                      variant="outlined"
                      disabled={commentPage === totalCommentPages}
                      onClick={() => setCommentPage((page) => Math.min(totalCommentPages, page + 1))}
                    >
                      Next
                    </Button>
                  </Stack>
                </Stack>    

            </AccordionDetails>
          </Accordion>

          <Stack direction="row" spacing={2} justifyContent="flex-end">
            <Button variant="outlined" color="inherit" onClick={onBack}>
              Cancel
            </Button>
            <Button variant="contained" color="primary">
              Save Changes
            </Button>
          </Stack>
        </Box>
      </CardContent>
    </Card>
  );
}
