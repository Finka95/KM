import { Box, Button, Typography, Grid, Card, CardMedia, CardContent } from "@mui/material";
import { FC } from "react";
import CameraAltIcon from '@mui/icons-material/CameraAlt';
import Photo from "../../shared/interfaces/photo";

type ProfileHeaderProps = {
  photos: Photo[]
}

const ProfileGallery: FC<ProfileHeaderProps> = ({ photos }) => {
  const photosToShow = photos.slice(0, 6) || [];
  const hasMorePhotos = photos.length > photosToShow.length;

  return (
    <Box sx={{ mb: 3 }}>
    <Box sx={{ display: 'flex', alignItems: 'center', mb: 1 }}>
      <Button
        onClick={() => alert("Adding photot...")}
        sx={{
          flex: '1 1 auto',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          borderRadius: '50px',
          backgroundColor: 'transparent',
          border: '1px dashed #ccc',
          textTransform: 'none',
          color: '#000',
          '&:hover': {
            backgroundColor: '#f0f0f0',
          }            
        }}
      >
        <CameraAltIcon sx={{ mr: 1 }} />
        <Typography sx={{
          fontSize: {
            md: '1rem',    // medium screens
          }           
        }}>
          Click to add a photo
        </Typography>
      </Button>
    </Box>
    <Grid container spacing={2}>
      {photosToShow.map((photo, index) => (
        <Grid item xs={6} sm={4} md={4} lg={4} key={index}>
          <Card sx={{ position: 'relative', paddingBottom: '100%', borderRadius: 5 }}>
            <CardMedia
              component="img"
              image={photo.photoURL}
              alt={`Photo ${index + 1}`}
              sx={{
                position: 'absolute',
                top: 0,
                left: 0,
                width: '100%',
                height: '100%',
                objectFit: 'contain',
              }}
            />
            {index === (photosToShow.length - 1) && hasMorePhotos && (
              <CardContent
                sx={{
                  display: 'flex',
                  alignItems: 'center',
                  justifyContent: 'center',
                  position: 'absolute',
                  top: 0,
                  left: 0,
                  width: '100%',
                  height: '100%',
                  backgroundColor: 'rgba(0, 0, 0, 0.5)',
                  color: 'rgb(255, 255, 255)',
                }}
                onClick={() => alert('There will be gallery')}
              >
                <Typography variant="h6" sx={{
                  textAlign: "center",
                  '@media (max-width:450px)': {
                    wordBreak: 'break-word',
                    fontSize: '12px',
                  }
                }}>
                  {photos.length - photosToShow.length} more photos
                </Typography>
              </CardContent>
            )}
          </Card>
        </Grid>
      ))}
    </Grid>
  </Box>
  );
};

export default ProfileGallery;