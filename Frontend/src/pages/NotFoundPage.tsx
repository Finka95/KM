import { Box, Typography } from "@mui/material";
import { FC } from "react";

const NotFoundPage: FC = () => {
  return (
  <Box 
    sx={{ 
      display: 'flex',
      justifyContent: 'center',
      alignItems: 'center',
      height: '100vh' 
    }}>
    <Typography>Page with this URL doesn't exist</Typography>
  </Box>
  )
}

export default NotFoundPage;