import { FC } from "react";
import { Box, Grid, Avatar, Typography } from "@mui/material";
import Photo from "../../shared/interfaces/photo";

type ProfileHeaderProps = {
  firstName: string;
  email: string;
  photos: Photo[];
};

const ProfileHeader: FC<ProfileHeaderProps> = ({ firstName, email, photos }) => {
  const avatarPhotoURL = photos.find(photo => photo.isAvatar)?.photoURL;

  const commonTypographyStyles = {
    overflow: 'hidden',
    textOverflow: 'ellipsis',
    whiteSpace: 'nowrap',
    width: '100%',
    textAlign: { xs: 'center', sm: 'left' },
  };

  return (
    <Box sx={{ mb: 3 }}>
      <Grid container spacing={2} alignItems="center" justifyContent="center">
        <Grid item xs={12} sm={3} container justifyContent={{ xs: 'center', sm: 'flex-start' }}>
          <Avatar
            sx={{
              width: { xs: 100, sm: 80 },
              height: { xs: 100, sm: 80 },
              mr: { xs: 0, sm: 2 },
              mb: { xs: 2, sm: 0 },
            }}
            src={avatarPhotoURL}
          />
        </Grid>
        <Grid item xs={12} sm={9} container direction="column" alignItems="center">
          <Typography variant="h5" sx={{ ...commonTypographyStyles, fontWeight: 'bold' }}>
            {firstName}
          </Typography>
          <Typography color="textSecondary" sx={commonTypographyStyles}>
            {email}
          </Typography>
        </Grid>
      </Grid>
    </Box>
  );
};

export default ProfileHeader;
