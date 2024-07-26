import { Box, Card, CardContent, IconButton, Typography } from "@mui/material";
import CreateOutlinedIcon from '@mui/icons-material/CreateOutlined';
import { FC } from "react";
import moment from "moment";
import { Subscription } from "../../shared/interfaces/subscription";

type ProfileSubscriptionProps = {
  subscription: Subscription
}

const ProfileSubscription: FC<ProfileSubscriptionProps> = ({ subscription }) => {
  const formatDate = (dateString: Date) => {
    return moment(dateString).format('DD.MM.YYYY');
  };

  return (
    <Box sx={{ mb: 3 }}>
      <Card variant="outlined" sx={{ position: 'relative' }}>
        <CardContent>
          <Box display="flex" alignItems="center" justifyContent="space-between">
            <Box>
              <Typography variant="h5" sx={{ mb: 1, fontWeight: "600" }}>Subscription</Typography>
              <Typography>Type: {String(subscription.subscriptionType)}</Typography>
              <Typography>Expires: {subscription.expiresAt && formatDate(subscription.expiresAt)}</Typography>
            </Box>
            <IconButton
              aria-label="manage subscription"
              onClick={() => alert("There will be subscription managment")}
              sx={{ position: 'absolute', top: 8, right: 8 }}
            >
              <CreateOutlinedIcon />
            </IconButton>
          </Box>
        </CardContent>
      </Card>
    </Box>
  );
};

export default ProfileSubscription;